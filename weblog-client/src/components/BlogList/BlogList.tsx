import {
  Title,
  Text,
  Card,
  Grid,
  Button,
  Menu,
  ActionIcon,
  rem,
  Loader,
} from "@mantine/core";
import {
  IconChevronRight,
  IconDots,
  IconTrash,
} from "@tabler/icons-react";
import classes from "./BlogList.module.css";
import { ConfirmationModal } from "../ConfirmationModal/ConfirmationModal";
import { useState } from "react";
import { Link } from "react-router-dom";
import { routes } from "../../routes/routes";
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { Blog } from '../../api/api';
import WeblogClient from "../../api/WeblogClient";
import { notifications } from '@mantine/notifications';

export const BlogList: React.FC = () => {
  const queryClient = useQueryClient();
  const blogClient = new WeblogClient();

  const { data: blogs, isLoading, isError, error } = useQuery({
    queryKey: ['blogs'],
    queryFn: async () => {
      const response = await blogClient.getBlogs();
      return response;
    }
  });

  const [deleteBlogId, setDeleteBlogId] = useState<number | null>(null);
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);

  const deleteBlogMutation = useMutation({
    mutationFn: (id: number) => blogClient.deleteBlog(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['blogs'] })
    },
  });

  const handleDeleteBlog = (blogId: number) => {
    setDeleteBlogId(blogId);
    setIsDeleteModalOpen(true);
  };

  const handleConfirmDelete = async () => {
    if (deleteBlogId !== null) {
      try {
        await deleteBlogMutation.mutateAsync(deleteBlogId);
        queryClient.invalidateQueries({ queryKey: ['blogs'] });
        notifications.show({
          title: 'Blog deleted',
          message: 'The blog post has been successfully deleted.',
        });
      } catch (error) {
        notifications.show({
          title: 'Error deleting blog',
          message: 'There was an error deleting the blog post.',
          color: 'red',
        });
      }
      setIsDeleteModalOpen(false);
    }
  };

  const handleCancelDelete = () => {
    setIsDeleteModalOpen(false);
  };

  if (isLoading) {
    return (
      <div className={classes.loadingContainer}>
        <Loader size="xl" />
      </div>
    );
  }

  if (isError) {
    notifications.show({
      title: 'Something went wrong',
      message: (error).message,
      color: 'red',
    });
  }

  const blogCards = (blogs ?? []).map((blog: Blog) => (
    <Card
      key={blog.id}
      shadow="sm"
      p="lg"
      radius="md"
      withBorder
      className={classes.blogCard}
    >
      <Card.Section>
        <div style={{ display: "flex", justifyContent: "space-between" }}>
          <Title order={3} className={classes.blogTitle} mr="sm">
            {blog.title}
          </Title>
          <Menu>
            <Menu.Target>
              <ActionIcon variant="light" mt="sm" mr="sm">
                <IconDots size={16} />
              </ActionIcon>
            </Menu.Target>
            <Menu.Dropdown>
              <Menu.Item
                leftSection={
                  <IconTrash
                    style={{ width: rem(16), height: rem(16) }}
                    stroke={1.5}
                  />
                }
                onClick={() => handleDeleteBlog(blog.id!)}
              >
                Delete
              </Menu.Item>
            </Menu.Dropdown>
          </Menu>
        </div>
      </Card.Section>
      <Text size="sm" c="dimmed" className={classes.blogContent}>
        {blog.truncatedContent}
      </Text>
      <Link to={`${routes.BLOGS_VIEW}/${blog.id}`}>
        <Button
          variant="light"
          radius="xl"
          size="xs"
          className={classes.readMoreLink}
        >
          Read more <IconChevronRight size={16} />
        </Button>
      </Link>
    </Card>
  ));

  return (
    <>
      <Title order={1} mb="xl" className={classes.pageTitle}>
        Browse{" "}
        <Text
          inherit
          variant="gradient"
          component="span"
          gradient={{ from: "pink", to: "yellow" }}
        >
          weblogs
        </Text>
      </Title>
      <Grid gutter="xl" className={classes.blogGrid}>
        {blogCards}
      </Grid>
      <ConfirmationModal
        opened={isDeleteModalOpen}
        onClose={handleCancelDelete}
        onConfirm={handleConfirmDelete}
        title="Delete Blog Post"
        text="Are you sure you want to delete this blog post?"
        confirmButtonColor="red"
      />
    </>
  );
};