import { Container, Anchor, Burger, Box, Select, Flex, Text } from '@mantine/core';
import { useDisclosure } from '@mantine/hooks';
import { useNavigate, useLocation } from 'react-router-dom';
import classes from './Header.module.css';
import { tabs } from './tabs/tabs';
import { useBaseUrl } from '../../contexts/BaseUrlContext';
import { apiUrl } from '../../api/WeblogClient';

export const Header: React.FC = () => {
  const [opened, { toggle }] = useDisclosure(false);
  const location = useLocation();
  const navigate = useNavigate();
  const { baseUrl, setBaseUrl } = useBaseUrl();

  const handleTabClick = (link: string) => {
    navigate(link);
  };

  const handleBaseUrlChange = (value: string | null) => {
    console.log(value);
    if (value !== null) {
      setBaseUrl(value);
    }
  };

  const mainItems = tabs.map((item) => (
    <Anchor<'a'>
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
        <Flex justify="space-between" align="center" className={classes.tabs}>
          {mainItems}
          <Flex align="center" className={classes.selectApiContainer}>
            <Text mr="sm" size="xs" c="gray" mt="-5px">
              Select API
            </Text>
            <Select
              data={[
                { value: apiUrl.LOCALHOST, label: 'localhost' },
                { value: apiUrl.DEV, label: 'weblog.runasp.net' },
              ]}
              value={baseUrl}
              onChange={handleBaseUrlChange}
              className={classes.baseUrlSelect}
              radius="xl"
              size="xs"
              mt="-10px"
            />
          </Flex>
        </Flex>
      </Box>
      <Burger
        opened={opened}
        onClick={toggle}
        className={classes.burger}
        size="sm"
        hiddenFrom="sm"
      />
    </Container>
  </header>
);
};
