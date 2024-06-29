import { Title, TextInput, Textarea, Button, Text, Box, Paper, Loader } from "@mantine/core";
import { useForm } from "@mantine/form";
import { useMutation, useQueryClient, useQuery } from "@tanstack/react-query";
import WeblogClient from "../../api/WeblogClient";
import { Blog, IBlog } from "../../api/api";
import { notifications } from "@mantine/notifications";
import { useNavigate, useParams } from "react-router-dom";
import { routes } from "../../routes/routes";
import { useEffect } from "react";
import { useBaseUrl } from "../../contexts/BaseUrlContext";

export const EditBlog: React.FC = () => {
  const { baseUrl } = useBaseUrl();
  const { blogId } = useParams<{ blogId: string }>();
  const queryClient = useQueryClient();
  const navigate = useNavigate();
  const blogClient = new WeblogClient(baseUrl);

  const { data: blog, isLoading, isError, error } = useQuery({
    queryKey: ["blog", blogId],
    queryFn: async () => {
      if (blogId) {
        const response = await blogClient.getBlog(parseInt(blogId));
        return response;
      }
      return null;
    },
  });

  const form = useForm({
    initialValues: {
      title: blog?.title ?? "",
      content: blog?.content ?? "",
    },
    validate: {
      title: (value) =>
        value.trim().length < 3 ? "Title must be at least 3 characters" : null,
      content: (value) =>
        value.trim().length > 0 ? null : "Content cannot be empty",
    },
  });

  const updateBlogMutation = useMutation({
    mutationFn: (updatedBlog: Blog) => blogClient.updateBlog(updatedBlog.id!, updatedBlog),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["blogs"] });
      queryClient.invalidateQueries({ queryKey: ["blog", blogId] });
      navigate(routes.BLOGS_VIEW);
      notifications.show({
        title: "Blog updated",
        message: "The blog post has been successfully updated.",
      });
    },
    onError: (error) => {
      notifications.show({
        title: "Error updating blog",
        message: (error).message,
        color: "red",
      });
    },
  });

  const handleSubmit = (values: IBlog) => {
    const updatedBlog: IBlog = {
      id: blog?.id,
      title: values.title,
      content: values.content,
    };
    updateBlogMutation.mutate(updatedBlog as Blog);
  };
    
  useEffect(() => {
    if (blog) {
      form.setValues({
        title: blog.title,
        content: blog.content,
      });
    }
  }, [blog]);

  if (isLoading) {
    return (
      <div>
        <Loader size="xl" />
      </div>
    );
  }

  if (isError) {
    notifications.show({
      title: "Something went wrong",
      message: (error).message,
      color: "red",
    });
    return null;
  }

  if (!blog) {
    return (
      <div>
        <Text>Blog not found</Text>
      </div>
    );
  }

  return (
    <>
      <Title order={1} mb="xl">
        Edit{" "}
        <Text
          inherit
          variant="gradient"
          component="span"
          gradient={{ from: "purple", to: "blue" }}
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
              loading={updateBlogMutation.isPending}
            >
              Save
            </Button>
          </form>
        </Paper>
      </Box>
    </>
  );
};
