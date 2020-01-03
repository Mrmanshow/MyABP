import Entity from './entity'
export default class ProductOrder extends Entity<number>{
    productId:number;
    count:number;
    payIntegration:number;
    idx:number;
    addressId:number;
    ContactWay:string;
    remarks:string;
    payPassword:string;

    constructor(){
        super();
        this.productId = 0;
        this.count = 1;
        this.payIntegration = 0;
        this.payPassword = '';
        this.addressId = 1;
    }
}