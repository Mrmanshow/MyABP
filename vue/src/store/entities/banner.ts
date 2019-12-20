import Entity from './entity'

export default class Banner extends Entity<number>{
    showBeginDate:Date;
    showEndDate:Date;
    bannerOrder: number;
    linkType: number;
    bannerType: number;
    bannerImg: string

    constructor(){
        super();
        this.bannerOrder = 0;
        this.showBeginDate = new Date();
        this.showEndDate = new Date();
        this.linkType = 0;
        this.bannerType = 0;
        this.bannerImg = "";
    }
}