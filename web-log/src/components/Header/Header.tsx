import { useState } from "react";
import { Container, Anchor, Group, Box } from "@mantine/core";
import { useNavigate } from "react-router-dom";
import classes from "./Header.module.css";
import { tabs } from "./tabs/tabs";

export const Header: React.FC = () => {
  const [active, setActive] = useState(0);
  const navigate = useNavigate();

  const handleTabClick = (index: number, link: string) => {
    setActive(index);
    navigate(link);
  };

  const mainItems = tabs.map((item, index) => (
    <Anchor<"a">
      href={item.link}
      key={item.label}
      className={classes.mainLink}
      data-active={index === active || undefined}
      onClick={(event) => {
        event.preventDefault();
        handleTabClick(index, item.link);
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
