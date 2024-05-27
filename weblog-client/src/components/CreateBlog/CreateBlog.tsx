import { Title, TextInput, Textarea, Button, Text, Box, Paper } from "@mantine/core";
import { useForm } from "@mantine/form";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import WeblogClient from "../../api/WeblogClient";
import { Blog, IBlog } from "../../api/api";
import { notifications } from "@mantine/notifications";
import { useNavigate } from "react-router-dom";
import { routes } from "../../routes/routes";
import { useBaseUrl } from "../../contexts/BaseUrlContext";

export const CreateBlog: React.FC = () => {
  const { baseUrl } = useBaseUrl();
  const queryClient = useQueryClient();
  const navigate = useNavigate();
  const blogClient = new WeblogClient(baseUrl);

  const form = useForm({
    initialValues: {
      title: "",
      content: "",
    },
    validate: {
      title: (value) =>
        value.trim().length < 3 ? "Title must be at least 3 characters" : null,
      content: (value) =>
        value.trim().length > 0 ? null : "Content cannot be empty",
    },
  });

  const createBlogMutation = useMutation({
    mutationFn: (blog: Blog) => blogClient.createBlog(blog),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["blogs"] });
      navigate(routes.BLOGS_VIEW);
      notifications.show({
        title: "Blog created",
        message: "The blog post has been successfully created.",
      });
    },
    onError: (error) => {
      notifications.show({
        title: "Error creating blog",
        message: (error).message,
        color: "red",
      });
    },
  });

  const handleSubmit = (values: IBlog) => {
    const newBlog: IBlog = {
      title: values.title,
      content: values.content,
    };
    createBlogMutation.mutate(newBlog as Blog);
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
      <Box>
        <Paper
          shadow="sm"
          p="xl"
          radius="xl"
          withBorder
          w={{ base: "100%", sm: 700 }}
        >
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
              maxRows={20}
              autosize
              {...form.getInputProps("content")}
            />

            <Button
              type="submit"
              variant="light"
              radius="xl"
              size="md"
              loading={createBlogMutation.isPending}
            >
              Save
            </Button>
          </form>
        </Paper>
      </Box>
    </>
  );
};
