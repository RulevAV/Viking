import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import './custom.css';
import {observer} from "mobx-react-lite";
import PrivateRoute from "./components/privateRoute/PrivateRoute";
import {useEffect} from "react";
import authorize from "./state/authorize";


const App = observer(()=> {
    useEffect(()=>{
        authorize.checkAuthorize();
    },[]);

    return (
        <Layout>
            <Routes>
                {AppRoutes.map((route, index) => {
                    const { element, ...rest } = route;
                    if (!route.isAuthorize){
                        return <Route key={index} {...rest} element={element} />;
                    } else {
                        return <Route key={index} element={<PrivateRoute/>}>
                            <Route {...rest} element={element} />;
                        </Route>
                    }
                })}
            </Routes>
        </Layout>
    )
});

export default App;
