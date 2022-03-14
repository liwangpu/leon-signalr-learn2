import { HttpClient } from '@angular/common/http';
import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import * as signalR from "@microsoft/signalr";
import * as faker from 'faker';

interface IFormValue {
    message: string;
}

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

    public form: FormGroup;
    // public canConnect: boolean = true;
    public connected: boolean;
    private token: string;
    private connection: signalR.HubConnection;
    private baseUrl = 'http://localhost:3201';
    public constructor(
        // private snackBar: MatSnackBar,
        private httpClient: HttpClient,
        private zone: NgZone,
        fb: FormBuilder
    ) {
        this.form = fb.group({
            username: [],
            password: [],
            message: [faker.random.words()]
        });
    }

    public ngOnInit(): void {
        // let lastLoginStr = localStorage.getItem('latest_login');
        // if (lastLoginStr) {
        //     this.form.patchValue(JSON.parse(lastLoginStr));
        // }

        this.connection = new signalR.HubConnectionBuilder()
            // .withUrl(`${this.baseUrl}/hub/chathub`, {
            .withUrl(`${this.baseUrl}/chathub`, {
                // accessTokenFactory() {
                //     return localStorage.getItem('access_token');
                // },
                transport: signalR.HttpTransportType.WebSockets
            })
            .withAutomaticReconnect()
            .build();

        this.connection.on("ReceiveMessage", (user, message) => {
            // console.log('messageReceived',message,aa);
            console.log(`用户:${user} 消息${message}`);
            // this.snackBar.open(`接收到消息:${message}`, null, { duration: 2000 });
        });

        this.connect();
    }

    public async login(): Promise<void> {
        // let account = this.form.value;
        // let url: string = `${this.baseUrl}/ids/connect/token`;
        // const body: FormData = new FormData();
        // body.set('grant_type', 'password');
        // body.set('client_id', 'server');
        // body.set('username', account.username);
        // body.set('password', account.password);
        // this.httpClient.post<any>(url, body).subscribe(res => {
        //     this.token = res.access_token;
        //     this.canConnect = true;
        //     localStorage.setItem('access_token', res.access_token);
        //     localStorage.setItem('latest_login', JSON.stringify(account));
        //     // this.snackBar.open(`登陆成功`, null, { duration: 2000 });
        // });
    }

    public connect(): void {
        if (this.connected) {
            console.log('已经连接,无需重复连接');
            return;
        }
        this.connection.start().then(() => {
            console.log('连接成功');
            this.connected = true;
        }).catch(err => {
            console.log('无法连接到服务器:', err);
            this.connected = false;
        });
    }

    public async disConnect(): Promise<void> {
        await this.connection.stop();
        console.log('主动断开连接');
        this.connected = false;
    }

    public sendMessage(): void {
        const value: IFormValue = this.form.value;
        this.connection.invoke("SendMessage", "leon", value.message)
            .then(() => {
                this.form.patchValue({ message: faker.random.words() });
            })
            .catch((err) => {
                return console.error(err.toString());
            });
    }

}
