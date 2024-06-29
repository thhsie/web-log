import { Blog, IBlog } from "../api/api";

let mockBlogs: IBlog[] = [
  // Initial mock blog data
];
let mockBlogId = 1;

export const getMockBlogs = (): IBlog[] => {
  return mockBlogs;
};

export const createMockBlog = (blog: Omit<Blog, "id">): Blog => {
  const newBlog: Blog = {
    id: mockBlogId++,
    ...blog,
  };
  mockBlogs.push(newBlog);
  return newBlog;
};

export const updateMockBlog = (id: number, blog: IBlog): IBlog => {
  mockBlogs = mockBlogs.map((b) => (b.id === id ? { ...b, ...blog } : b));
  return blog;
};

export const deleteMockBlog = (id: number): void => {
  mockBlogs = mockBlogs.filter((b) => b.id !== id);
};
