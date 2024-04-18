import { SyntheticEvent, useEffect, useState } from "react";
import { Autocomplete, TextField, Box } from "@mui/material";
import { useSearchParams } from "react-router-dom";
import capitalizeFirstLetter from "../../utils/helperFunctions/capitalizeFirstLetter";
import UserProfileAvatar from "./UserProfileAvatar";

const AutoCompleteField = ({
  options,
  label,
  multiple = false,
  renderIcon = false,
}: DynamicTable.AutoCompleteFieldProps) => {
  const [value, setValue] = useState<Global.Option | Global.Option[] | null>(
    multiple ? [] || {} : null
  );
  const [searchParams, setSearchParams] = useSearchParams();

  const paramKey = `${label}Ids`;
  const newLabel = capitalizeFirstLetter(label) + `${multiple ? "s" : ""}`;

  const handleChange = (
    _: SyntheticEvent<Element, Event>,
    newValue: Global.Option[] | Global.Option | null
  ) => {
    console.log(newValue);
    const selectedValue = multiple
      ? (newValue as Global.Option[] | null)
      : newValue
      ? [newValue as Global.Option]
      : null;
    console.log(selectedValue);
    setValue(selectedValue);

    const param = new URLSearchParams(searchParams);
    param.delete(paramKey);

    if (selectedValue && selectedValue.length !== 0) {
      const ids = selectedValue.map((option) => option.value);
      param.append(paramKey, ids.join(`,`));
    }

    setSearchParams(param);
  };

  useEffect(() => {
    if (!searchParams.get(paramKey)) return;
    var selectedOptions;
    if (paramKey === "UserIds") {
      const ids = searchParams.get(paramKey)?.split(",");
      selectedOptions = options.filter((option) => ids?.includes(option.value));
    } else {
      const ids = searchParams
        .get(paramKey)
        ?.split(",")
        .map((param) => parseInt(param));

      selectedOptions = options.filter((option) =>
        ids?.includes(option.value as number)
      );
    }

    console.log(selectedOptions, options);

    setValue(multiple ? selectedOptions : selectedOptions[0]);
  }, [searchParams, options, multiple, paramKey]);

  return (
    <Autocomplete
      options={options}
      multiple={multiple}
      limitTags={2}
      value={value}
      getOptionLabel={(option) => option.label}
      renderOption={(props, option) => (
        <Box component="li" {...props}>
          {renderIcon ? (
            <UserProfileAvatar name={option.label} />
          ) : (
            option.label
          )}
        </Box>
      )}
      renderInput={(params) => (
        <TextField
          {...params}
          label={
            newLabel === "Statuss"
              ? "Status"
              : newLabel === "LeaveTypes"
              ? "Leave Types"
              : newLabel
          }
          placeholder={`Select ${
            newLabel === "Statuss"
              ? "Status"
              : newLabel === "LeaveTypes"
              ? "Leave Types"
              : newLabel
          }`}
        />
      )}
      filterSelectedOptions
      onChange={handleChange}
    />
  );
};

export default AutoCompleteField;
