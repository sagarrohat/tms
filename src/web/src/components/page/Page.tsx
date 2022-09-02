import React from "react";
import { Helmet } from "react-helmet";
import { Layout } from "antd";
import style from "./Page.module.scss";

type Props = {
  children: React.ReactNode;
  title: string;
};

function Page(props: Props) {
  const { children, title } = props;
  const commonTitle = "TMS";
  const pageTitle = title ? `${title} - ${commonTitle}` : commonTitle;
  return (
    <div>
      <Helmet>
        <title>{pageTitle}</title>
      </Helmet>
      <Layout>{children}</Layout>
    </div>
  );
}

export default Page;
