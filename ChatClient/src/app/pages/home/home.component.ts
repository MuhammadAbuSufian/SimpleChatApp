import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {Router} from '@angular/router';
import {UserService} from '../../services/user.service';
import {MessageService} from '../../services/message.service';
import {HubConnection, HubConnectionBuilder} from '@aspnet/signalr';
import {environment} from '../../../environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  // @ts-ignore
  @ViewChild('messagebox') private chatArea: ElementRef;
  userDetails = JSON.parse( localStorage.getItem('login-user') as string);
  users: any;
  chatUser: any;
  messages: any[] = [];
  displayMessages: any[] = [];
  message: any;
  hubConnection: any;
  UserConnectionID: any;
  sender: any;
  connectedUsers: any[] = [];
  deletedId = '';
  constructor(
    private router: Router,
    private userService: UserService,
    private messageService: MessageService
  ) { }

  ngOnInit(): void {
    console.log(this.userDetails);
    this.messageService.getUserReceivedMessages(this.userDetails.id).subscribe((item: any) => {
      if ( item ){
        this.messages = item;
        this.messages.forEach( (message: any) => {
          message.type = message.receiver === this.userDetails.id ? 'recieved' : 'sent';
        });
        console.log(this.messages);
      }
    });
    this.userService.getAll().subscribe(
      (users: any) => {
        if (users){
          this.users = users.filter( (x: any) => x.email !== this.userDetails.email);
          this.users.forEach((item: any) => {
            item.isActive = false;
          });
          this.makeItOnline();
        }
      },
      err => {
        console.log(err);
      },
    );

    this.message = '';
    this.hubConnection = new HubConnectionBuilder().withUrl(environment.chatHubUrl).build();
    const self = this;

    this.hubConnection.start()
      .then( () => {
        self.hubConnection.invoke('OnConnect', this.userDetails.id, this.userDetails.firstName, this.userDetails.email)
          .then(() => console.log('User Sent Successfully'))
          .catch((err: any) => console.error(err));

        this.hubConnection.on('OnConnect', (users: any) => {
          this.connectedUsers = users;
          this.makeItOnline();
        });

        this.hubConnection.on('OnDisconnect', (users: any) => {
          this.connectedUsers = users;
          this.users.forEach( (item: any) => {
            item.isOnline = false;
          });
          this.makeItOnline();
        });
      })
      .catch((err: any) => console.log(err));

    this.hubConnection.on('UserConnected', (connectionId: any) => this.UserConnectionID = connectionId);

    this.hubConnection.on('receiveMessageAll', (connectionId: any, message: any) => {
      message.type = 'recieved';

      this.messages.push( message );
    });

    this.hubConnection.on('ReceiveDM', (connectionId: any, message: any) => {
      message.type = 'recieved';
      this.messages.push( message );
      this.chatUser  = this.users.find((x: any) => x.id == message.sender);
      this.users.forEach((item: any) => {
        item.isActive = false;
      });
      const user = this.users.find((x: any) => x.id === this.chatUser.id);
      user.isActive = true;
      this.displayMessages = this.messages.filter(x => (( x.type === 'sent' && x.receiver === this.chatUser.id) || ( x.type === 'recieved' && x.sender === this.chatUser.id)) && x.isActive === true);
      this.scrollToBottom();
    });

    this.hubConnection.on('DeleteForEveryoneDM', (connectionId: any, message: any) => {
      message.isActive = false;
      this.messages.forEach((msg) => {
        if ( msg.id === message.id){
          msg.isActive = false;
        }
      });
      this.chatUser  = this.users.find((x: any) => x.id == message.sender);
      this.displayMessages = this.messages.filter(x => (( x.type === 'sent' && x.receiver === this.chatUser.id) || ( x.type === 'recieved' && x.sender === this.chatUser.id)) && x.isActive === true);
    });
  }

  openChat(user: any): void{
    this.users.forEach( (item: any) => {
      item.isActive = false;
    });
    user.isActive = true;
    this.chatUser = user;
    // tslint:disable-next-line:max-line-length
    this.displayMessages = this.messages.filter(x => (( x.type === 'sent' && x.receiver === this.chatUser.id) || ( x.type === 'recieved' && x.sender === this.chatUser.id)) && x.isActive === true);
    this.scrollToBottom();
  }

  makeItOnline(): void{
    if (this.connectedUsers && this.users){
      this.connectedUsers.forEach((item: any) => {
        const user = this.users.find((x: any) => x.email == item.username);
        if (user){
          user.isOnline = true;
        }
      });
    }
  }

  SendDirectMessage(): void{
    if ( this.message != '' && this.message.trim() != '')
    {
      const msg = {
        sender: this.userDetails.id,
        receiver: this.chatUser.id,
        messageDate: new Date(),
        type: 'sent',
        content: this.message,
        isActive: true
      };
      this.messages.push(msg);
      this.displayMessages = this.messages.filter(x => (( x.type === 'sent' && x.receiver === this.chatUser.id) || ( x.type === 'recieved' && x.sender === this.chatUser.id)) && x.isActive === true);
      this.scrollToBottom();
      this.hubConnection.invoke('SendMessageToUser', msg)
        .then(() => console.log('Message to user Sent Successfully'))
        .catch((err: any) => console.error(err));
      this.message = '';
      this.scrollToBottom();
    }
  }


  onLogout(): void {
    this.hubConnection.invoke('RemoveOnlineUser' , this.userDetails.id)
      .then(() => {
        this.messages.push('User Disconnected Successfully');
      })
      .catch((err: any) => console.error(err));
    localStorage.removeItem('token');
    localStorage.removeItem('login-user');
    this.router.navigate(['/login']);
  }

  setDelete(id: string): void{
    this.deletedId = id;
  }

  deleteForEveryone(content: string, messageDate: any): void {
    let deleteMessage: any = null;
    this.messages.forEach( msg => {
      if ( msg.content === content && msg.messageDate === messageDate ) {
        deleteMessage = msg;
      }
    });
    deleteMessage.type = undefined;
    this.hubConnection.invoke('DeleteForEveryone', deleteMessage)
      .then(() => {
        deleteMessage.isActive = false;
        this.displayMessages = this.messages.filter(x => (( x.type === 'sent' && x.receiver === this.chatUser.id) || ( x.type === 'recieved' && x.sender === this.chatUser.id)) && x.isActive === true);
        this.scrollToBottom();
        console.log('Message deleted');
      })
      .catch((err: any) => console.error(err));
  }
  deleteForMe(): void {

  }

  scrollToBottom(): void{
    setTimeout(() => {
      this.chatArea.nativeElement.scrollTop = this.chatArea.nativeElement.scrollHeight;
    }, 100 );
  }

}
