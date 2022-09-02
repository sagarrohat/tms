import React from "react";
import { Row, Col } from "antd";
import { useSelector } from "react-redux";
import { getTasksByUsers } from "./selectors";
import { renderPieChartLabel } from "./utils";
import { Empty } from "antd";
import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  PieChart,
  Pie,
  Cell,
  ResponsiveContainer,
} from "recharts";
import {
  COMPLETED,
  CANCELLED,
  OVERDUE,
  LOW,
  NORMAL,
  HIGH,
  CHART_COLORS_MAP,
} from "./constants";

export default function Dashboard() {
  var tasksByUsers = useSelector(getTasksByUsers);

  if (tasksByUsers == null || tasksByUsers == undefined) {
    return <Empty />;
  }

  const tasksByPriority = [
    {
      name: LOW,
      Completed: tasksByUsers?.LowCompletedCount,
      Cancelled: tasksByUsers?.LowCancelledCount,
      Overdue: tasksByUsers?.LowOverDueCount,
    },
    {
      name: NORMAL,
      Completed: tasksByUsers?.NormalCompletedCount,
      Cancelled: tasksByUsers?.NormalCancelledCount,
      Overdue: tasksByUsers?.NormalOverDueCount,
    },
    {
      name: HIGH,
      Completed: tasksByUsers?.HighCompletedCount,
      Cancelled: tasksByUsers?.HighCancelledCount,
      Overdue: tasksByUsers?.HighOverDueCount,
    },
  ];

  const tasksByStatus = [
    {
      name: OVERDUE,
      value: tasksByUsers?.OverDueCount,
      color: CHART_COLORS_MAP.Overdue,
    },
    {
      name: CANCELLED,
      value: tasksByUsers?.CancelledCount,
      color: CHART_COLORS_MAP.Cancelled,
    },
    {
      name: COMPLETED,
      value: tasksByUsers?.CompletedCount,
      color: CHART_COLORS_MAP.Completed,
    },
  ];

  return (
    <div style={{ marginTop: 10 }}>
      <Row>
        <Col span={12} className="d-flex justify-content-center">
          <div className="ant-statistic-title" style={{ textAlign: "center" }}>
            TASKS BY PRIORITY
          </div>
          <ResponsiveContainer width="100%" height={300}>
            <BarChart
              width={500}
              height={300}
              data={tasksByPriority}
              layout="vertical"
              margin={{
                top: 5,
                right: 30,
                left: 20,
                bottom: 5,
              }}
            >
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis type="number" allowDecimals={false} />
              <YAxis dataKey="name" type="category" />
              <Tooltip />
              <Legend />
              <Bar dataKey={COMPLETED} fill={CHART_COLORS_MAP.Completed} />
              <Bar dataKey={CANCELLED} fill={CHART_COLORS_MAP.Cancelled} />
              <Bar dataKey={OVERDUE} fill={CHART_COLORS_MAP.Overdue} />
            </BarChart>
          </ResponsiveContainer>
        </Col>
        <Col span={12} className="d-flex justify-content-center">
          <div className="ant-statistic-title" style={{ textAlign: "center" }}>
            TASKS BY STATUS
          </div>
          <ResponsiveContainer width="100%" height={300}>
            <PieChart width={500} height={300}>
              <Pie
                data={tasksByStatus}
                dataKey="value"
                cx="50%"
                cy="50%"
                outerRadius={90}
                labelLine={false}
                label={renderPieChartLabel}
              >
                {tasksByStatus.map((item, index) => (
                  <Cell key={`cell-${index}`} fill={item.color} />
                ))}
              </Pie>
              <Tooltip />
              <Legend />
            </PieChart>
          </ResponsiveContainer>
        </Col>
      </Row>
    </div>
  );
}
