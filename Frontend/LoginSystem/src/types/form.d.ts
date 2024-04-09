declare namespace FormTypes {
  type FormInputProps = {
    name: string;
    control: Control<unknown>;
    label: string;
    value?: string | null | undefined;
  };
}
