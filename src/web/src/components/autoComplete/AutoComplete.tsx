import React from "react";
import { AutoComplete as AntAutoComplete } from "antd";
import styles from "./AutoComplete.module.scss";

type Props = {
  keyFieldName: string;
  valueFieldName: string;
  defaultKey: string;
  options: Array<any>;
  onChange?: Function;
};

function AutoComplete(props: Props) {
  let mappedOptions = [];
  const {
    options,
    keyFieldName,
    valueFieldName,
    defaultKey,
    onChange,
    ...otherProps
  } = props;

  if (options) {
    mappedOptions = options.map((element) => ({
      key: element[keyFieldName],
      value: element[valueFieldName],
    }));
  }

  const findByKey = (key: string) => {
    return mappedOptions.find((e) => e.key == key);
  };

  const findByValue = (value: string) => {
    return mappedOptions.find((e) => e.value == value);
  };

  let defaultValue = null;
  if (defaultKey !== undefined) {
    defaultValue = findByKey(defaultKey)?.value;
  }

  const filterOption = (inputText: string, option: any) => {
    return option?.value?.toUpperCase().indexOf(inputText.toUpperCase()) !== -1;
  };

  const onSelect = (value: string, option: any) => {
    let selectedOption = findByValue(value);

    onChange(selectedOption.key);
  };

  return (
    <AntAutoComplete
      options={mappedOptions}
      defaultValue={defaultValue}
      className={styles.autoComplete}
      filterOption={filterOption}
      onSelect={onSelect}
      {...otherProps}
    />
  );
}

export default AutoComplete;
