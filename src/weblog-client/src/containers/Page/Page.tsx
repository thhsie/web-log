import { Container, Flex, FlexProps, Box } from "@mantine/core";
import classes from "./Page.module.css";

interface PageProps extends FlexProps {
  children: React.ReactNode;
}

export const Page: React.FC<PageProps> = ({ children, ...props }) => {
  return (
    <Flex direction="row" className={classes.pageContainer} {...props}>
      <Box className={classes.pageContent}>
        <Container className={classes.pageBody}>{children}</Container>
      </Box>
    </Flex>
  );
};
