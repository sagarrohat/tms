// @flow
import { List, Empty } from "antd";
import { useSelector } from "react-redux";
import TasksItem from "./tasksItem/TasksItem";
import { getTasks } from "../../../home/selectors";

function TasksList(props) {
  const tasks = useSelector(getTasks);

  const { editClicked, deleteClicked } = props;

  const renderItem = (item, index) => {
    return (
      <TasksItem
        onClick={() => editClicked(item, false)}
        deleteClicked={deleteClicked}
        editClicked={editClicked}
        item={item}
        index={index}
        isLastItem={tasks?.length - 1 === index}
      />
    );
  };

  return (
    <div>
      {tasks ? (
        <List
          style={{ minHeight: "38vh" }}
          dataSource={tasks}
          renderItem={(item, index) => renderItem(item, index)}
        />
      ) : (
        <Empty />
      )}
    </div>
  );
}

export default TasksList;
