import { Title, TextInput, Textarea, Button } from "@mantine/core";

export const CreateBlog: React.FC = () => {
  return (
    <>
      <Title order={1} mb="xl">
        Create a new blog
      </Title>
      <TextInput label="Title" placeholder="Blog title" mb="md" />
      <Textarea label="Content" placeholder="Blog content" mb="md" />
      <Button variant="gradient" gradient={{ from: "pink", to: "yellow" }}>
        Create Blog
      </Button>
    </>
  );
};
