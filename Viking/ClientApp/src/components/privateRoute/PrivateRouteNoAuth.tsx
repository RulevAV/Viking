import {observer} from "mobx-react-lite";
import { Navigate, Outlet, useLocation} from 'react-router-dom';
import Authorize from "../../state/authorize";
import {useEffect} from "react";

const PrivateRouteNoAuth = observer(()=> {
    const location = useLocation(); // получаем текущий маршрут с помощью хука useLocation()

    useEffect(() => {
        Authorize.isAuthenticated = null;
        Authorize.checkAuthorize();
        return ()=>{
            Authorize.isAuthenticated = null;
        }
    }, []);
    if (Authorize.isAuthenticated === null) {
        return (<>Loading...</>);
    }
    if (Authorize.isAuthenticated === true) {
        return (<Navigate to="/" state={{from: location}} replace/>);
    }

    return (<Outlet/>);
});

export default PrivateRouteNoAuth;
