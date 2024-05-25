import { useNavigate, useParams } from "react-router-dom";
import { Title, Text, ActionIcon, Tooltip, rem } from "@mantine/core";
import { IconArrowLeft } from "@tabler/icons-react";
import { routes } from "../../routes/routes";
import { blogs } from "../../mocks/blogList";

// Temporary blog model
export interface Blog {
  id: number;
  title: string;
  content: string;
}

export const BlogPost: React.FC = () => {
  const { blogId } = useParams<{ blogId: string }>();
  const navigate = useNavigate();

  //const blog = fetchBlogData(parseInt(blogId || "0"));
  const blog = blogs[0];

  const handleGoBack = () => {
    navigate(routes.BLOGS_VIEW);
  };

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
      <Text size="lg">{blog.content}</Text>
    </>
  );
};
