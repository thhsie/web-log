import { Card, Title, Menu, ActionIcon, rem, Button, Text } from "@mantine/core";
import { IconDots, IconTrash, IconChevronRight } from "@tabler/icons-react";
import { Link } from "react-router-dom";
import { Blog } from "../../api/api";
import { routes } from "../../routes/routes";
import classes from './BlogCard.module.css';

export const BlogCard: React.FC<{ blog: Blog; onClick: (blogId: number) => void }> = ({
    blog,
    onClick,
  }) => (
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
                onClick={() => onClick(blog.id!)}
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
  );