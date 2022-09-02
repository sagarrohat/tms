import moment from "moment";
import { useDispatch } from "react-redux";
import { Row, Col, Avatar, Progress, Tooltip, Menu, Dropdown } from "antd";
import styles from "./TasksItem.module.css";
import { formatName } from "../utils";
import {
  UpCircleTwoTone,
  DownCircleTwoTone,
  MinusCircleTwoTone,
  PushpinOutlined,
  PushpinFilled,
} from "@ant-design/icons";
import { pinTask, unpinTask } from "../../../actionCreators";

type Props = {
  item: Object,
  editClicked: Function,
  deleteClicked: Function,
  onClick: Function,
  index: Number,
  isLastItem: Boolean,
};

function TasksItem(props: Props) {
  const { item, editClicked, deleteClicked, onClick, isLastItem } = props;

  const dispatch = useDispatch();

  const taskComplete = () => {
    editClicked({ ...item, Status: 2 }, true);
  };

  const taskCancel = () => {
    editClicked({ ...item, Status: 3 }, true);
  };

  const taskDelete = () => {
    deleteClicked(item.Id);
  };

  const itemOptions = (
    <Menu>
      <Menu.Item key="1" onClick={taskComplete}>
        Complete
      </Menu.Item>
      <Menu.Item key="2" onClick={taskCancel}>
        Cancel
      </Menu.Item>
      <Menu.Item key="3" onClick={taskDelete}>
        Delete
      </Menu.Item>
    </Menu>
  );

  function renderAvatar(item) {
    let avatarColor = "#808080"; // Grey
    let initials = "U";
    let avatarTooltip = "Unassigned";

    if (item.AssignedUserId !== undefined && item.AssignedUserId !== null) {
      avatarColor = "#004A80"; // Blue
      let userFullName = `${item.AssignedUserFirstName} ${item.AssignedUserLastName}`;

      avatarTooltip = userFullName;
      initials = formatName(userFullName);
    }

    return (
      <Tooltip title={avatarTooltip}>
        <Avatar style={{ backgroundColor: avatarColor, float: "right" }}>
          {initials}
        </Avatar>
      </Tooltip>
    );
  }

  function renderProgress(item) {
    let strokeColor = "#FF0000"; // Red
    let percentageCompleted = item.PercentageCompleted ?? 0.0;

    if (percentageCompleted > 30.0 && percentageCompleted <= 70) {
      strokeColor = "#F56A00"; // Blue
    } else if (percentageCompleted > 70.0) {
      strokeColor = "#52C41A"; // Green
    }

    return (
      <Progress
        style={{ float: "right" }}
        percent={percentageCompleted}
        steps={10}
        strokeColor={strokeColor}
        trailColor="#808080"
      />
    );
  }

  function renderDate(item) {
    let date = moment.utc(item.DueDate);

    if (item.IsOverDue) {
      return (
        <Tooltip title={date.format("DD MMM YY")}>
          <span style={{ float: "right" }}>{date.fromNow()}</span>
        </Tooltip>
      );
    } else if (moment.utc().isSame(date, "d")) {
      return <span style={{ float: "right" }}>today</span>;
    }

    return <span style={{ float: "right" }}>{date.format("DD MMM YY")}</span>;
  }

  function renderTitle(item) {
    let textColor = "#000000"; // Black
    if (item.IsOverDue) {
      textColor = "#FF0000"; // Red
    }

    return <span style={{ color: textColor }}>{item.Title}</span>;
  }

  function renderPriority(item) {
    // Normal
    let priorityIcon = <MinusCircleTwoTone twoToneColor="#E8AF3C" />;
    let priorityTooltip = "Normal";

    switch (item.Priority) {
      case 1: // Low
        priorityIcon = <DownCircleTwoTone twoToneColor="#376BEF" />;
        priorityTooltip = "Low";
        break;
      case 3: // High
        priorityIcon = <UpCircleTwoTone twoToneColor="#DA573E" />;
        priorityTooltip = "High";
        break;
    }

    return <Tooltip title={priorityTooltip}>{priorityIcon}</Tooltip>;
  }

  const pinClicked = (event) => {
    dispatch(pinTask({ id: item.Id }));
    event.stopPropagation();
  };

  const unpinClicked = (event) => {
    dispatch(unpinTask({ id: item.Id }));
    event.stopPropagation();
  };

  const renderPushpin = (item) => {
    let pinIcon = <PushpinOutlined onClick={(event) => pinClicked(event)} />;
    if (item.IsPinned) {
      pinIcon = <PushpinFilled onClick={(event) => unpinClicked(event)} />;
    }
    return (
      <div style={{ cursor: "pointer", fontSize: 20, float: "right" }}>
        {" "}
        {pinIcon}{" "}
      </div>
    );
  };

  let className = isLastItem ? styles.lastItem : styles.item;
  return (
    <Dropdown overlay={itemOptions} trigger={["contextMenu"]}>
      <div onClick={onClick} className={className}>
        <Row align="middle">
          <Col span={14}>{renderTitle(item)}</Col>
          <Col span={1}>{renderPriority(item)}</Col>
          <Col span={5}>{renderProgress(item)}</Col>
          <Col span={2}>{renderDate(item)}</Col>
          <Col span={1}>{renderAvatar(item)}</Col>
          <Col span={1}>{renderPushpin(item)}</Col>
        </Row>
      </div>
    </Dropdown>
  );
}

export default TasksItem;
