import './custom.css';
import {observer} from "mobx-react-lite";
import React, {useEffect, useState} from "react";
import {Layout, Menu, Button, theme} from 'antd';
import {
    MenuFoldOutlined,
    MenuUnfoldOutlined,
    UploadOutlined,
    UserOutlined,
    VideoCameraOutlined,
    PoweroffOutlined, PlusCircleFilled
} from '@ant-design/icons';
import {Link, Navigate, Route, Routes, useLocation, useNavigate} from 'react-router-dom';
import PrivateRouteAuth from './components/privateRoute/PrivateRouteAuth';
import AppRoutes from './AppRoutes';
import PrivateRouteNoAuth from './components/privateRoute/PrivateRouteNoAuth';
import {NavItem, NavLink} from "reactstrap";
import authorize from "./state/authorize";
import workout from "./state/workout";

const {Header, Sider, Content} = Layout;

const App = observer(() => {
    const navigate = useNavigate();
    useEffect(() => {
        authorize.checkAuthorize();
    }, []);

    useEffect(() => {
        if (authorize.isAuthenticated === false) {
            navigate('/login');
        } else {
            navigate('/Trains')
        }

    }, [authorize.isAuthenticated]);
    const location = useLocation();
    let path = location.pathname
    let curPath = null
    AppRoutes.filter(t => t.isAuthorize).forEach((u, index) => path === u.path ? curPath = index : null);
    const [collapsed, setCollapsed] = useState(false);
    const {
        token: {colorBgContainer},
    } = theme.useToken();

    let tab = AppRoutes.filter(e => e.type !== "header").map((e, index) => {
        return {
            key: index,
            icon: <NavItem>
                <NavLink tag={Link} to={e.path}>{e.icon}</NavLink>
            </NavItem>,
            label: e.name,
        }
    })

    if (authorize.isAuthenticated === null) {
        return (<>Loading...</>);
    }

    if (!authorize.isAuthenticated) {
        return (<>
            <Routes>
                {AppRoutes.filter(e => e.type === "header").map((route, index) => {
                    const {element, ...rest} = route;
                    return <Route key={index}   {...rest} element={element}/>;
                })}
            </Routes>
        </>);
    }

    return (
        <Layout className={"body"}>
            <Sider trigger={null} collapsible collapsed={collapsed}>
                <div className="demo-logo-vertical"/>
                <Menu
                    theme="dark"
                    mode="inline"
                    selectedKeys={['' + curPath]}
                    items={tab}
                />
            </Sider>
            <Layout>
                <Header style={{
                    padding: 0,
                    background: colorBgContainer,
                    justifyContent: "space-between",
                    display: "flex"
                }}>
                    <div>
                        <Button
                            type="text"
                            icon={collapsed ? <MenuUnfoldOutlined/> : <MenuFoldOutlined/>}
                            onClick={() => setCollapsed(!collapsed)}
                            style={{
                                fontSize: '16px',
                                width: 64,
                                height: 64,
                            }}
                        />
                        <Button
                            type='text'
                            icon={<PlusCircleFilled/>}
                            title='Добавить тренеровку'
                            onClick={() => workout.createNewWorkout('Новая тренировка')}
                        >
                        </Button>
                    </div>
                    <Button
                        icon={<PoweroffOutlined/>}
                        onClick={() => authorize.logOut()}
                        className={"me-2"}
                        style={{
                            fontSize: '16px',
                            width: 64,
                            height: 64,
                        }}
                    />
                </Header>
                <Content
                    style={{
                        margin: '24px 16px',
                        padding: 24,
                        minHeight: 280,
                        background: colorBgContainer,
                    }}
                >
                    <Routes>
                        {AppRoutes.filter(e => e.type !== "header").map((route, index) => {
                            const {element, ...rest} = route;
                            if (route.isAuthorize === undefined) {
                                return <Route key={index} {...rest} element={element}/>;
                            }

                            if (route.isAuthorize) {
                                return <Route key={index} element={<PrivateRouteAuth/>}>
                                    <Route  {...rest} element={element}/>;
                                </Route>
                            } else {
                                return <Route key={index} element={<PrivateRouteNoAuth/>}>
                                    <Route  {...rest} element={element}/>;
                                </Route>
                            }
                        })}
                    </Routes>
                </Content>
            </Layout>
        </Layout>
    )
});

export default App;
