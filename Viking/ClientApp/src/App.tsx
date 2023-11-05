import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import './custom.css';
import counter from "./state/counter";
import {observer} from "mobx-react-lite";
import Toto from "./components/Todo";


const App = observer(()=> {
    return (
        <Layout>
            {counter.count}
            <div>
                <button onClick={()=> counter.incriment()}>+</button>
                <button onClick={()=> counter.decriment()}>-</button>
            </div>
            <Toto/>
            <Routes>
                {AppRoutes.map((route, index) => {
                    const { element, ...rest } = route;
                    return <Route key={index} {...rest} element={element} />;
                })}
            </Routes>
        </Layout>
    )
});

// const App = ()=> {
//     return (
//         <Layout>
//             {counter.count}
//             <div>
//                 <button onClick={()=> counter.incriment()}>+</button>
//                 <button onClick={()=> counter.decriment()}>-</button>
//             </div>
//             <Routes>
//                 {AppRoutes.map((route, index) => {
//                     const { element, ...rest } = route;
//                     return <Route key={index} {...rest} element={element} />;
//                 })}
//             </Routes>
//         </Layout>
//     )
// };
export default App;
