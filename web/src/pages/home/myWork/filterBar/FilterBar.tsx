import React, { useState, useEffect } from "react";
import { useDispatch } from "react-redux";
import { Row, Col, DatePicker, Input, Tooltip } from "antd";
import { fetchTasks } from "../../actionCreators";
import styles from "./FilterBar.module.css";

const { RangePicker } = DatePicker;

function FilterBar() {
  const dispatch = useDispatch();

  // Filters
  const [keyword, setKeyword] = useState(null);
  const [duration, setDuration] = useState([null, null]);

  const keywordChanged = (e) => {
    setKeyword(e.target.value === "" ? null : e.target.value);
  };

  const dateChanged = (
    dates: [moment, moment],
    dateStrings: [string, string]
  ) => {
    let from = dateStrings[0];
    let to = dateStrings[1];

    if (from === "" || to === "") {
      setDuration([null, null]);
    } else {
      setDuration([from, to]);
    }
  };

  useEffect(() => {
    let payload = {
      filters: {
        Keyword: keyword,
        From: duration[0],
        To: duration[1],
      },
    };
    dispatch(fetchTasks(payload));
  }, [keyword, duration]);

  return (
    <Row gutter={24}>
      <Col span={18}>
        <Input onPressEnter={keywordChanged} placeholder="Keyword..." />
      </Col>
      <Col span={6}>
        <RangePicker style={{ width: "100%" }} onChange={dateChanged} />
      </Col>
    </Row>
  );
}

export default FilterBar;
