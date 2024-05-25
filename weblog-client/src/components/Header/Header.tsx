import { Container, Anchor, Group, Box } from "@mantine/core";
import { useNavigate, useLocation } from "react-router-dom";
import classes from "./Header.module.css";
import { tabs } from "./tabs/tabs";

export const Header: React.FC = () => {
  const location = useLocation();
  const navigate = useNavigate();

  const handleTabClick = (link: string) => {
    navigate(link);
  };

  const mainItems = tabs.map((item) => (
    <Anchor<"a">
      href={item.link}
      key={item.label}
      className={classes.mainLink}
      data-active={location.pathname === item.link || undefined}
      onClick={(event) => {
        event.preventDefault();
        handleTabClick(item.link);
      }}
    >
      {item.label}
    </Anchor>
  ));

  return (
    <header className={classes.header}>
      <Container className={classes.inner}>
        <Box className={classes.links} visibleFrom="sm">
          <Group gap={0} justify="flex-end" className={classes.tabs}>
            {mainItems}
          </Group>
        </Box>
      </Container>
    </header>
  );
};
