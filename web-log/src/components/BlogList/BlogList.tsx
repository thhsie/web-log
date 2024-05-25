import {
  Title,
  Text,
  Card,
  Grid,
  Button,
  Menu,
  ActionIcon,
  rem,
} from "@mantine/core";
import { IconChevronRight, IconDots, IconTrash } from "@tabler/icons-react";
import classes from "./BlogList.module.css";
import { ConfirmationModal } from "../ConfirmationModal/ConfirmationModal";
import { useState } from "react";
import { blogs } from "../../mocks/blogList";
import { Link } from "react-router-dom";
import { routes } from "../../routes/routes";

export const BlogList: React.FC = () => {
  //const [blogs, setBlogs] = useState(/* data */);
  //const [deleteBlogId, setDeleteBlogId] = useState<number | null>(null);
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);

  const handleDeleteBlog = (blogId: number) => {
    //setDeleteBlogId(blogId);
    setIsDeleteModalOpen(true);
  };

  const handleConfirmDelete = () => {
    //if (deleteBlogId !== null) {
    // Remove blog
    //setBlogs(blogs.filter((blog) => blog.id !== deleteBlogId));
    //}
    setIsDeleteModalOpen(false);
  };

  const handleCancelDelete = () => {
    setIsDeleteModalOpen(false);
  };

  const blogCards = blogs.map((blog) => (
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
          <Title order={3} className={classes.blogTitle}>
            {blog.title}
          </Title>
          <Menu>
            <Menu.Target>
              <ActionIcon variant="light">
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
                onClick={() => handleDeleteBlog(blog.id)}
              >
                Delete
              </Menu.Item>
            </Menu.Dropdown>
          </Menu>
        </div>
      </Card.Section>
      <Text size="sm" c="dimmed" className={classes.blogContent}>
        {blog.content}
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
