import Entity from './entity'

export default class Notifications extends Entity<number>{
    status:number;

    constructor(){
        super();
        this.status = 0;
    }
}