import axios, { AxiosInstance } from 'axios';
import { Blog, IBlog } from './api.ts';

class WeblogClient {
    private http: AxiosInstance;

    constructor(baseUrl?: string) {
        this.http = axios.create({
            baseURL: baseUrl ?? "http://localhost:8080",
        });
    }

    async getBlogs(): Promise<Blog[]> {
        const response = await this.http.get('/blogs');
        return response.data.map((data: IBlog) => Blog.fromJS(data));
    }

    async getBlog(id: number): Promise<Blog> {
        const response = await this.http.get(`/blogs/${id}`);
        return Blog.fromJS(response.data);
    }

    async createBlog(blog: Blog): Promise<Blog> {
        const response = await this.http.post('/blogs', blog);
        return Blog.fromJS(response.data);
    }

    async updateBlog(id: number, blog: Blog): Promise<Blog> {
        const response = await this.http.put(`/blogs/${id}`, blog);
        return Blog.fromJS(response.data);
    }

    async deleteBlog(id: number): Promise<void> {
        await this.http.delete(`/blogs/${id}`);
    }
}

export default WeblogClient;
