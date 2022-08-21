import React from "react";
import styles from "./Container.module.scss";

type Props = {
  children: React.ReactNode;
};

function Container(props: Props) {
  return <div className={styles.container}>{props.children}</div>;
}

export default Container;
