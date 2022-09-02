import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { loginUser } from "../../core/features/authorization/actionCreators";
import { Button, Input, Form, Alert } from "antd";
import { UserOutlined, LockOutlined } from "@ant-design/icons";
import { getLoginAlert } from "../../core/features/authorization/selectors";
import styles from "./Login.module.css";

export default function Login() {
  const dispatch = useDispatch();

  const loginAlert = useSelector(getLoginAlert);

  const login = ({ email, pd }) => {
    dispatch(loginUser({ emailAddress: email, pd: pd }));
  };

  return (
    <div className={styles.page}>
      <div className={styles.content}>
        <img src="logo512.png" className={styles.logo} alt="logo" />
        {loginAlert && (
          <Alert
            message={loginAlert.message}
            type={loginAlert.type}
            showIcon
            closable
          />
        )}
        <br />
        <Form size="large" autoComplete="off" onFinish={login}>
          <Form.Item name="email">
            <Input prefix={<UserOutlined />} placeholder="Email" required />
          </Form.Item>
          <Form.Item name="pd">
            <Input
              prefix={<LockOutlined />}
              type="password"
              placeholder="Password"
              required
            />
          </Form.Item>
          <Form.Item>
            <Button className={styles.button} type="primary" htmlType="submit">
              Log in
            </Button>
          </Form.Item>
        </Form>
      </div>
    </div>
  );
}
