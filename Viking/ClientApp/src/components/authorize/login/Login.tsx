import { LockOutlined, UserOutlined } from '@ant-design/icons';
import {Button, Checkbox, Flex, Form, Input} from 'antd';
import authorize, {LoginType} from "../../../state/authorize";
import {useEffect} from "react";

const boxStyle: React.CSSProperties = {
    height: "300px",
    borderRadius: 6,
    border: '1px solid #40a9ff',
};

const Login = ()=> {
    const [form] = Form.useForm();
    useEffect(()=>{
        form.setFieldValue("login", "Viking1");
        form.setFieldValue("password", "Viking1");

    },[])
    const logInBt = (values: LoginType) => {
        authorize.login(values);
    };
    return (<>
            <button onClick={()=> authorize.test()}>asdasd</button>
                <Flex gap="middle" align="center" vertical>
                    <Flex style={boxStyle} className={"col-9 col-sm-7 col-md-5 col-lg-4 col-xl-3"} justify={"center"}
                          align={"center"}>
                        <Form
                            form={form}
                            name="normal_login"
                            className="login-form login "
                            initialValues={{remember: true}}
                            onFinish={logInBt}
                        >
                            <Form.Item
                                name="login"
                                rules={[{required: true, message: 'Please input your Login!  '}]}
                            >
                                <Input prefix={<UserOutlined className="site-form-item-icon"/>} placeholder="Login"/>
                            </Form.Item>
                            <Form.Item
                                name="password"
                                rules={[{required: true, message: 'Please input your Password!'}]}
                            >
                                <Input
                                    prefix={<LockOutlined className="site-form-item-icon"/>}
                                    type="password"
                                    placeholder="Password"
                                />
                            </Form.Item>
                            <Form.Item>
                                <Form.Item name="remember" valuePropName="checked" noStyle>
                                    <Checkbox>Remember me</Checkbox>
                                </Form.Item>

                                <a className="login-form-forgot" href="">
                                    Forgot password
                                </a>
                            </Form.Item>

                            <Form.Item>
                                <Button type="primary" htmlType="submit" className="login-form-button m-2">
                                    Log in
                                </Button>
                                Or <a className="m-1" href="" onClick={() => authorize.test()}>register now!</a>
                            </Form.Item>
                        </Form>
                    </Flex>
                </Flex>
        </>
    );
};

export default Login;
