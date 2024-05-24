import {
  Title,
  Text,
  Anchor,
  Card,
  Grid,
  Pagination,
  useMantineTheme,
} from "@mantine/core";
import { useState } from "react";
import { IconChevronRight } from "@tabler/icons-react";
import classes from "./BlogList.module.css";

export const BlogList: React.FC = () => {
  const [activePage, setActivePage] = useState(1);

  const blogs = [
    {
      id: 1,
      title: "Blog 1",
      content: "This is the content of Blog 1.",
    },
    {
      id: 2,
      title: "Blog 2",
      content: "This is the content of Blog 2.",
    },
    {
      id: 3,
      title: "Blog 3",
      content: "This is the content of Blog 3.",
    },
    {
      id: 4,
      title: "Blog 4",
      content: "This is the content of Blog 4.",
    },
    {
      id: 5,
      title: "Blog 5",
      content: "This is the content of Blog 5.",
    },
  ];

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
        <Title order={3} className={classes.blogTitle}>
          {blog.title}
        </Title>
      </Card.Section>
      <Text size="sm" c="dimmed" className={classes.blogContent}>
        {blog.content}
      </Text>
      <Anchor
        href={`/blogs/${blog.id}`}
        size="sm"
        mt="md"
        component="a"
        className={classes.readMoreLink}
      >
        Read more <IconChevronRight size={16} />
      </Anchor>
    </Card>
  ));

  return (
    <>
      <Title order={1} mb="xl" className={classes.pageTitle}>
        View Blogs
      </Title>
      <Grid gutter="xl" className={classes.blogGrid}>
        {blogCards}
      </Grid>
      {/* <Pagination
        page={activePage}
        onChange={setActivePage}
        total={Math.ceil(blogs.length / 4)}
        position="center"
        mt="xl"
        className={classes.pagination}
      /> */}
    </>
  );
};
