import React from "react";
import { Layout, Menu, Dropdown, Typography, Avatar, Button } from "antd";
import { useDispatch, useSelector } from "react-redux";
import { logoutUser } from "../../core/features/authorization/actionCreators";
import { getUser } from "../../core/features/authorization/selectors";
import styles from "./AppBar.module.scss";
import { BellOutlined } from "@ant-design/icons";

const { Title } = Typography;
const { Header } = Layout;

function AppBar() {
  const dispatch = useDispatch();

  const user = useSelector(getUser);

  const handleLogout = () => {
    dispatch(logoutUser());
  };

  const userOptions = (
    <Menu>
      <Menu.Item key="0" onClick={handleLogout}>
        Logout
      </Menu.Item>
    </Menu>
  );

  const getAvatarLetters = (name) => {
    let avatarLetters;
    const nameParts = name.trim().split(" ");
    avatarLetters = nameParts[0].charAt(0);
    if (nameParts.length > 1) {
      avatarLetters += nameParts[1].charAt(0);
    }
    return avatarLetters;
  };

  return (
    <Header className={styles.header}>
      <img src="logo512.png" className={styles.logo} alt="logo" />
      <span className={styles.navOptions}>
        <Dropdown
          overlay={userOptions}
          trigger={["click"]}
          className={styles.userMenu}
          placement="bottomLeft"
        >
          <div onClick={(e) => e.preventDefault()}>
            <Avatar style={{ backgroundColor: "#f56a00" }} size="large">
              {getAvatarLetters(user.FullName)}
            </Avatar>
          </div>
        </Dropdown>
      </span>
      {/*
      <span className={styles.navOptions} style={{ marginRight: 15 }}>
        <Button
          style={{ backgroundColor: "#004a80", color: "#F1F2F3" }}
          type="text"
          size="large"
          icon={<BellOutlined />}
        >
          Notifications
        </Button>
      </span>
      */}
    </Header>
  );
}

export default AppBar;
