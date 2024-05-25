import { useState } from "react";
import {
  SegmentedControl,
  useMantineColorScheme,
  useComputedColorScheme,
  MantineColorScheme,
  ActionIcon,
  Text,
  Title,
  Select,
} from "@mantine/core";
import { IconChevronRight, IconChevronLeft } from "@tabler/icons-react";
import cx from "clsx";
import classes from "./Sidebar.module.css";

export const Sidebar: React.FC = () => {
  const [isExpanded, setIsExpanded] = useState(true);
  const [dataSource, setDataSource] = useState<"mock" | "api">("mock");
  const { colorScheme, setColorScheme } = useMantineColorScheme();
  useComputedColorScheme("light", { getInitialValueInEffect: true });

  return (
    <nav
      className={cx(classes.sidebar, {
        [classes.expanded]: isExpanded,
        [classes.reduced]: !isExpanded,
      })}
    >
      <div className={classes.sidebarMain}>
        <div
          className={cx(classes.segmentedControl, {
            [classes.hidden]: !isExpanded,
          })}
        >
          <div className={classes.preferencesTitle}>
            <Text
              inherit
              variant="gradient"
              component="span"
              gradient={{ from: "pink", to: "yellow" }}
            >
              Preferences
            </Text>
          </div>
          <SegmentedControl
            value={colorScheme}
            onChange={(value: string) =>
              setColorScheme(value as MantineColorScheme)
            }
            transitionTimingFunction="ease"
            fullWidth
            data={[
              { label: "Light", value: "light" },
              { label: "Dark", value: "dark" },
            ]}
          />
          <div className={classes.spacer} />
          <Title order={5} className={classes.dataSourceTitle}>
            Select data source
          </Title>
          <Select
            value={dataSource}
            onChange={(value) => setDataSource(value as "mock" | "api")}
            data={[
              { value: "mock", label: "Mock Data" },
              { value: "api", label: "API Data" },
            ]}
            placeholder="Select data source"
            className={classes.dataSourceSelect}
          />
        </div>
        <div className={classes.toggleButton}>
          <ActionIcon
            variant="filled"
            color={isExpanded ? "blue" : "gray"}
            onClick={() => setIsExpanded(!isExpanded)}
          >
            {isExpanded ? (
              <IconChevronLeft size={24} />
            ) : (
              <IconChevronRight size={24} />
            )}
          </ActionIcon>
        </div>
      </div>
    </nav>
  );
};
