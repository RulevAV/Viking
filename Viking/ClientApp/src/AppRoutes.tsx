import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import Login from "./components/authorize/login/Login";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />,
    isAuthorize: true
  },{
    path: '/login',
    element: <Login />,
    isAuthorize: false
  }
];

export default AppRoutes;
