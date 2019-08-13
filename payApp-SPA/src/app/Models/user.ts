import { Wish } from './wish';

export class User {
    public Id: number;
    public UserName: string;
    public Password: string;
    public RememberMe: boolean;
    public Income: number;
    public Costs: number;
    public UserWishes: Wish[];

}
