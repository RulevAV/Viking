import {makeAutoObservable} from "mobx";

class Todo {
    todos = [{id: 1, name:"qwe", complate : false}, {id: 2, name:"asd",complate : false}, {id: 3, name:"zxc", complate : false}]

    a = 1;
    b = 2;

    constructor() {
        makeAutoObservable(this);
    }

    addTodo(todo: any){
        this.todos.push(todo);
    }
    removeTodo(id: any){
        this.todos = this.todos.filter(u=>u.id !== id);
    }
    complateTodo(id: any){
        this.todos.forEach(u=> {
            if (u.id === id) {
                u.complate =  !u.complate;
            }
        });
    }

    get sum() {
        return this.a + this.b;
    }

}

export default new Todo()
