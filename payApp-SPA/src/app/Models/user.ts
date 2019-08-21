import { Wish } from './wish';

export class User {
    public id: number;
    public userName: string;
    public password: string;
    public rememberMe: boolean;
    public income: number;
    public costs: number;
    public userWishes: Wish[];
    public email: string;

}
