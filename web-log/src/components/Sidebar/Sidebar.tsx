import { useState } from "react";
import {
  SegmentedControl,
  useMantineColorScheme,
  useComputedColorScheme,
  MantineColorScheme,
  ActionIcon,
} from "@mantine/core";
import {
  IconDatabaseImport,
  Icon2fa,
  IconSettings,
  IconChevronRight,
  IconChevronLeft,
} from "@tabler/icons-react";
import cx from "clsx";
import classes from "./Sidebar.module.css";

const tabs = [
  { link: "", label: "Databases", icon: IconDatabaseImport },
  { link: "", label: "Authentication", icon: Icon2fa },
  { link: "", label: "Other Settings", icon: IconSettings },
];

export const Sidebar: React.FC = () => {
  const [active, setActive] = useState("");
  const [isExpanded, setIsExpanded] = useState(true);
  const { colorScheme, setColorScheme } = useMantineColorScheme();
  useComputedColorScheme("light", { getInitialValueInEffect: true });

  const links = tabs?.map((item) => (
    <a
      className={cx(classes.link, {
        [classes.active]: item.label === active,
        [classes.hidden]: !isExpanded,
      })}
      href={item.link}
      key={item.label}
      onClick={(event) => {
        event.preventDefault();
        setActive(item.label);
      }}
    >
      <item.icon className={classes.linkIcon} />
      <span>{item.label}</span>
    </a>
  ));

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
        {/* {links} */}
      </div>
    </nav>
  );
};
