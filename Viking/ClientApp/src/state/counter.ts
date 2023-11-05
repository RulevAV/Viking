import {makeAutoObservable} from "mobx";

class Counter {
    count = 0;
    constructor() {
        makeAutoObservable(this);
    }
    incriment(){
        this.count = this.count + 1;
    }
    decriment(){
        this.count = this.count - 1;
    }

}

export default new Counter()
