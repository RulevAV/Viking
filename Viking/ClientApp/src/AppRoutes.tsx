import  Trains  from "./components/Trains";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import Login from "./components/authorize/login/Login";
import {UploadOutlined, UserOutlined, VideoCameraOutlined} from "@ant-design/icons";
import React from "react";

const AppRoutes = [
  {
    index: true,
    element: <Home />,
    isAuthorize: true,
    icon:<UserOutlined/>,
    name:'Home',
    path: '/',
  },
  {
    path: '/Trains',
    element: <Trains />,
    isAuthorize: true,
    icon:<VideoCameraOutlined/>,
    name:'Тренировки'
  },
  {
    path: '/fetch-data',
    element: <FetchData />,
    isAuthorize: true,
    icon:<UserOutlined/>,
    name:'Fetch-data'

  },{
    path: '/login',
    element: <Login />,
    isAuthorize: false,
    icon:<UploadOutlined/>,
    name:'Login',
    type: "header",
  }
];

export default AppRoutes;

