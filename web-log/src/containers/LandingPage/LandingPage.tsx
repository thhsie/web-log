import { Title, Text, Anchor } from "@mantine/core";
import classes from "./LandingPage.module.css";

export const LandingPage: React.FC = () => {
  return (
    <>
      <Title className={classes.title} ta="center" mt={100}>
        Welcome to{" "}
        <Text
          inherit
          variant="gradient"
          component="span"
          gradient={{ from: "pink", to: "yellow" }}
        >
          Weblog
        </Text>
      </Title>
      <Text c="dimmed" ta="center" size="lg" maw={580} mx="auto" mt="xl">
        <Anchor
          href="https://github.com/thhsie/web-log"
          size="lg"
          target="_blank"
          rel="noopener noreferrer"
        >
          This technical assignment
        </Anchor>{" "}
        showcases use of React concepts (router, components, state, props,
        lifecycle methods, hooks). Furthermore, TanStack Query, a library
        designed to replace global state managers such as Redux and MobX is
        incorporated. More information on how they provide this better approach
        can be found{" "}
        <Anchor
          href="https://tanstack.com/query/latest/docs/framework/react/guides/does-this-replace-client-state"
          size="lg"
          target="_blank"
          rel="noopener noreferrer"
        >
          here
        </Anchor>{" "}
      </Text>
    </>
  );
};
