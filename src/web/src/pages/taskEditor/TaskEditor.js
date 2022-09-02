import React, { useEffect, useRef, useState } from "react";
import { Form, Alert, Input, Button, Select, DatePicker, Slider } from "antd";
import { useSelector, useDispatch } from "react-redux";
import { getAssignableUsers } from "../home/selectors";
import { getItem, getApiError } from "./selectors";
import { taskCreateOrUpdate } from "./actionCreators";
import { AutoComplete } from "../../components";
import {
  convertToFormFields,
  convertFromFormFields,
  getAssignedUser,
} from "./utils";

const { TextArea } = Input;

function TaskEditor(props) {
  const dispatch = useDispatch();

  const allUsers = useSelector(getAssignableUsers);
  const apiError = useSelector(getApiError);
  const taskItem = useSelector(getItem);

  let assignableUsers = [];

  //Exclude All Users Option
  if (allUsers && allUsers.length > 0) {
    assignableUsers = [...allUsers.slice(1, allUsers.length)];
  }

  const [assignedUser, setAssignedUser] = useState(
    getAssignedUser(taskItem, assignableUsers)
  );
  const form = useRef(null);

  useEffect(() => {
    form.current.resetFields();
    form.current.setFieldsValue(convertToFormFields(taskItem));
  }, [taskItem]);

  const onAssignedUserChanged = (option) => {
    setAssignedUser(option);
  };

  const onFinish = (values) => {
    let newValues = convertFromFormFields(
      taskItem,
      values,
      assignedUser.UserId
    );
    dispatch(taskCreateOrUpdate({ ...newValues }, false));
  };

  return (
    <div>
      {apiError && (
        <Alert
          style={{ marginBottom: 15 }}
          message={apiError}
          type="error"
          showIcon
          closable
        />
      )}
      <Form
        ref={form}
        onFinish={onFinish}
        labelCol={{
          span: 6,
        }}
        wrapperCol={{
          span: 0,
        }}
        layout="horizontal"
      >
        <Form.Item
          name="Title"
          label="Title"
          rules={[{ required: true, message: "Title is required field." }]}
        >
          <Input />
        </Form.Item>
        <Form.Item name="Priority" label="Priority">
          <Select>
            <Select.Option value="1">Low</Select.Option>
            <Select.Option value="2">Normal</Select.Option>
            <Select.Option value="3">High</Select.Option>
          </Select>
        </Form.Item>
        <Form.Item
          name="DueDate"
          label="Due Date"
          rules={[{ required: true, message: "Due Date is required field." }]}
        >
          <DatePicker />
        </Form.Item>
        <Form.Item label="Assigned User">
          <AutoComplete
            keyFieldName="UserId"
            valueFieldName="FullName"
            options={assignableUsers}
            defaultOption={assignedUser}
            onChange={onAssignedUserChanged}
          />
        </Form.Item>
        <Form.Item name="PercentageCompleted" label="Completed %">
          <Slider />
        </Form.Item>
        <Form.Item name="Description" label="Description">
          <TextArea rows={3} />
        </Form.Item>
        <Button
          type="primary"
          htmlType="submit"
          shape="round"
          style={{ float: "right" }}
        >
          Save
        </Button>
      </Form>
    </div>
  );
}

export default TaskEditor;
