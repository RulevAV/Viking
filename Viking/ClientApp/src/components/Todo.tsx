import {observer} from "mobx-react-lite";
import todo from "../state/todo";

const Toto = observer(()=> {
    return (
        <div>
            {todo.sum}
            {todo.todos.map(e=>
            <div className="todo" key={e.id}>
                <input type="checkbox" checked={e.complate} onChange={() => todo.complateTodo(e.id)}/>
                {e.name}
                <button onClick={()=> todo.removeTodo(e.id)}>X</button>
            </div>)}
        </div>
       )
});

export default Toto;
