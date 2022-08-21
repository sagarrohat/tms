import React from "react";
import { AutoComplete as AntAutoComplete } from "antd";
import styles from "./AutoComplete.module.scss";

type Props = {
  keyFieldName: string;
  valueFieldName: string;
  defaultOption: Object;
  options: Array<any>;
  onChange?: Function;
};

function AutoComplete(props: Props) {
  let options = [];
  const {
    options: initialOptions,
    keyFieldName,
    valueFieldName,
    defaultOption,
    onChange,
    ...otherProps
  } = props;
  if (initialOptions) {
    options = initialOptions.map((element) => ({
      key: element[keyFieldName],
      value: element[valueFieldName],
    }));
  }

  let defaultValue = null;
  if (defaultOption !== undefined && defaultOption !== null) {
    defaultValue = defaultOption[valueFieldName];
  }

  const filterOption = (inputText: string, option: any) => {
    return option?.value?.toUpperCase().indexOf(inputText.toUpperCase()) !== -1;
  };

  const onSelect = (value: string, option: any) => {
    let selectedOption = initialOptions.find(
      (element) => element[valueFieldName] == value
    );
    onChange(selectedOption);
  };

  return (
    <AntAutoComplete
      options={options}
      defaultValue={defaultValue}
      className={styles.autoComplete}
      filterOption={filterOption}
      onSelect={onSelect}
      {...otherProps}
    />
  );
}

export default AutoComplete;
