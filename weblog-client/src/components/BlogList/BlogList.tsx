import {
  Title,
  Text,
  Grid,
  Loader,
  Box,
} from "@mantine/core";
import classes from "./BlogList.module.css";
import { ConfirmationModal } from "../ConfirmationModal/ConfirmationModal";
import { useState } from "react";
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { Blog } from '../../api/api';
import WeblogClient from "../../api/WeblogClient";
import { notifications } from '@mantine/notifications';
import { BlogCard } from "../BlogCard/BlogCard";

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
    <BlogCard key={blog.id} blog={blog} onClick={handleDeleteBlog} />
  ));

  return (
    <>
      <Title order={1} mb="sm">
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
      <Box>
        <Grid gutter="md">
          {blogCards}
        </Grid>
      </Box>

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