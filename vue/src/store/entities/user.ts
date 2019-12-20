import Entity from './entity'
export default class User extends Entity<number>{
    password:string;
    gender:number;

    constructor(){
        super();
        this.gender = 0;
    }
}