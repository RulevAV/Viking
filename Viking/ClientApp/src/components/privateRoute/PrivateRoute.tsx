import {observer} from "mobx-react-lite";
import { Navigate, Outlet, useLocation} from 'react-router-dom';
import Authorize from "../../state/authorize";
import {useEffect} from "react";

const PrivateRoute = observer(()=> {
    const location = useLocation(); // получаем текущий маршрут с помощью хука useLocation()

    useEffect(()=>{
        Authorize.checkAuthorize();
    },[])
    return (
        // если пользователь авторизован, то рендерим дочерние элементы текущего маршрута, используя компонент Outlet
        Authorize.isAuthenticated === true ?
            <Outlet />
            // если пользователь не авторизован, то перенаправляем его на маршрут /login с помощью компонента Navigate
            // свойство replace указывает, что текущий маршрут будет заменен на новый, чтобы пользователь не мог вернуться
            // обратно, используя кнопку "назад" в браузере.
            :
            <Navigate to="/login" state={{ from: location }} replace />
    );
});

export default PrivateRoute;
