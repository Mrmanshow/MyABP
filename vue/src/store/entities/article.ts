import  Entity from './entity'

export default  class article extends Entity<number> {
    title:string;
    img:string;
    sort:number;
    type:number;
    status:number;

    constructor() {
        super();
        this.sort = 0;
        this.type = 1;
        this.status = 0;
    }
}