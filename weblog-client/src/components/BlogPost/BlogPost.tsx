import { useNavigate, useParams } from "react-router-dom";
import { Title, ActionIcon, Tooltip, rem, Loader, Text, Box } from "@mantine/core";
import { IconArrowLeft } from "@tabler/icons-react";
import { routes } from "../../routes/routes";
import { useQuery } from "@tanstack/react-query";
import WeblogClient from "../../api/WeblogClient";
import { notifications } from "@mantine/notifications";
import ReactMarkdown from 'react-markdown';
import classes from './BlogPost.module.css'

export const BlogPost: React.FC = () => {
  const { blogId } = useParams<{ blogId: string }>();
  const navigate = useNavigate();
  const blogClient = new WeblogClient();

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

  const handleGoBack = () => {
    navigate(routes.BLOGS_VIEW);
  };

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
      <div
        style={{
          display: "flex",
          alignItems: "center",
          marginBottom: "var(--mantine-spacing-xl)",
          marginLeft: rem(-120),
        }}
      >
        <Tooltip label="Go back" position="right" withArrow>
          <ActionIcon
            variant="light"
            onClick={handleGoBack}
            size="lg"
            color="gray"
            style={{ marginRight: rem(50) }}
          >
            <IconArrowLeft size={24} />
          </ActionIcon>
        </Tooltip>
        <Title order={1} ml="xl">
          {blog.title}
        </Title>
      </div>
      <Box className={classes.blogPostContainer}>
        <ReactMarkdown>{blog.content}</ReactMarkdown>
      </Box>
    </>
  );
};
