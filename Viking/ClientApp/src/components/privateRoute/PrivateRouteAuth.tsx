import {observer} from "mobx-react-lite";
import { Navigate, Outlet, useLocation} from 'react-router-dom';
import Authorize from "../../state/authorize";
import {useEffect} from "react";

const PrivateRouteAuth = observer(()=> {
    const location = useLocation(); // получаем текущий маршрут с помощью хука useLocation()
    useEffect(()=>{
    },[]);
    if (Authorize.isAuthenticated === null){
        return (<>Loading...</>);
    }
    if (Authorize.isAuthenticated === false){
        return (<Navigate to="/" state={{ from: location }} replace />);
    }
    return (<Outlet />);
});

export default PrivateRouteAuth;
