import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import Layout  from './components/Layout';
import './custom.css';
import {observer} from "mobx-react-lite";
import PrivateRouteNoAuth from "./components/privateRoute/PrivateRouteNoAuth";
import {useEffect} from "react";
import authorize from "./state/authorize";
import PrivateRouteAuth from "./components/privateRoute/PrivateRouteAuth";


const App = observer(()=> {
    useEffect(()=>{
         authorize.checkAuthorize();
    },[]);
    return (
        // @ts-ignore
        <Layout>
            <Routes>
                {AppRoutes.map((route, index) => {
                    const { element, ...rest } = route;
                    if (route.isAuthorize === undefined){
                        return  <Route key={index} {...rest} element={element} />;
                    }

                    if (route.isAuthorize){
                        return <Route key={index} element={<PrivateRouteAuth/>}>
                            <Route {...rest} element={element} />;
                        </Route>
                    } else {
                        return <Route key={index} element={<PrivateRouteNoAuth/>}>
                            <Route  {...rest} element={element} />;
                        </Route>
                    }
                })}
            </Routes>
        </Layout>
    )
});

export default App;
