import { useSelector, useDispatch } from "react-redux";
import React, { useState, useEffect } from "react";
import { Tabs, Layout, Row, Col, Button, Drawer } from "antd";
import { Page, AppBar, AutoComplete, Container } from "../../components";
import TaskEditor from "../taskEditor/TaskEditor";
import MyWork from "./myWork/MyWork";
import Dashboard from "./dashboard/Dashboard";
import { userChanged, toggleTab } from "./actionCreators";
import { editorOpen, editorClose } from "../taskEditor/actionCreators";
import { getUser } from "../../core/features/authorization/selectors";
import { getAssignableUsers, getActiveTab } from "./selectors";
import { getTaskEditorVisibility } from "../taskEditor/selectors";
import { HOME_TAB_MYWORK, HOME_TAB_DASHBOARD } from "./constants";

import { PlusOutlined } from "@ant-design/icons";

import styles from "./Home.module.css";

const { TabPane } = Tabs;
const { Content } = Layout;

function Home() {
  const dispatch = useDispatch();

  const isTaskEditorVisible = useSelector(getTaskEditorVisibility);

  const assignableUsers = useSelector(getAssignableUsers);
  const activeTab = useSelector(getActiveTab);
  const [user, setUser] = useState(useSelector(getUser));

  const onUserChanged = (option) => {
    setUser(option);
  };

  const onClose = () => {
    dispatch(editorClose());
  };

  const fabClicked = () => {
    dispatch(editorOpen({}));
  };

  useEffect(() => {
    let payload = {
      filters: {
        AssignedUserId: user.UserId,
      },
    };
    dispatch(userChanged(payload));
  }, [user]);

  function tabChanged(key) {
    dispatch(toggleTab(key));
  }

  return (
    <div>
      <Page title="Home">
        <AppBar />
        <Content className={styles.outerContainer}>
          <Container>
            <Row>
              <Col span={4}>
                <AutoComplete
                  keyFieldName="UserId"
                  valueFieldName="FullName"
                  defaultOption={user}
                  options={assignableUsers}
                  onChange={onUserChanged}
                />
              </Col>
              <Col span={2} offset={18}>
                <Button
                  style={{ float: "right" }}
                  type="primary"
                  shape="circle"
                  size="middle"
                  onClick={fabClicked}
                  icon={<PlusOutlined />}
                />
              </Col>
            </Row>
          </Container>
          <br />
          <Container>
            <Tabs defaultActiveKey={activeTab} onChange={tabChanged}>
              <TabPane tab="Dashboard" key={HOME_TAB_DASHBOARD}>
                <Dashboard />
              </TabPane>
              <TabPane tab="My Work" key={HOME_TAB_MYWORK}>
                <MyWork />
              </TabPane>
            </Tabs>
          </Container>
        </Content>
      </Page>
      <Drawer
        title="Task Editor"
        width={520}
        onClose={onClose}
        visible={isTaskEditorVisible}
        className={styles.drawer}
      >
        <TaskEditor />
      </Drawer>
    </div>
  );
}

export default Home;
