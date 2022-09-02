import { Row, Col } from "antd";
import FilterBar from "./filterBar/FilterBar";
import TasksList from "./tasksList/TasksList";
import { useDispatch } from "react-redux";
import {
  editorOpen,
  taskCreateOrUpdate,
  taskDelete,
} from "../../taskEditor/actionCreators";
import moment from "moment";
import styles from "./MyWork.module.css";

export default function MyWork() {
  const dispatch = useDispatch();

  const editClicked = (item, directUpdate) => {
    if (directUpdate) {
      dispatch(
        taskCreateOrUpdate(
          { ...item, DueDate: moment.utc(item?.DueDate) },
          true
        )
      );
    } else {
      dispatch(editorOpen(item));
    }
  };

  const deleteClicked = (id) => {
    dispatch(taskDelete({ id }));
  };

  return (
    <div>
      <FilterBar />
      <br />
      <Row>
        <Col span={24}>
          <TasksList deleteClicked={deleteClicked} editClicked={editClicked} />
        </Col>
      </Row>
    </div>
  );
}
