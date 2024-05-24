import { Title, TextInput, Textarea, Button, Text } from "@mantine/core";
import { useForm } from "@mantine/form";

export const CreateBlog: React.FC = () => {
  const form = useForm({
    initialValues: {
      title: "",
      content: "",
    },
    validate: {
      title: (value) =>
        value.trim().length < 3 ? "Title must be at least 3 characters" : null,
      content: (value) =>
        value.trim().length < 10
          ? "Content must be at least 10 characters"
          : null,
    },
  });

  const handleSubmit = (values) => {
    console.log(values);
  };

  return (
    <>
      <Title order={1} mb="xl">
        Create a new{" "}
        <Text
          inherit
          variant="gradient"
          component="span"
          gradient={{ from: "pink", to: "yellow" }}
        >
          blog
        </Text>
      </Title>
      <form onSubmit={form.onSubmit(handleSubmit)}>
        <TextInput
          label="Title"
          placeholder="Blog title"
          mb="md"
          {...form.getInputProps("title")}
        />
        <Textarea
          label="Content"
          placeholder="Blog content"
          mb="md"
          minRows={20}
          autosize
          {...form.getInputProps("content")}
        />

        <Button type="submit" variant="light" radius="xl" size="md">
          Save
        </Button>
      </form>
    </>
  );
};
