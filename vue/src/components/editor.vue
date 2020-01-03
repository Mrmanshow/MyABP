<template>
    <div ref="content"></div>
</template>

<script lang="ts">
import { Component, Prop, Watch, Emit } from 'vue-property-decorator';
import AbpBase from '../lib/abpbase'
import WangEditor from "wangeditor";

@Component
export default class Editor extends AbpBase {
    @Prop({type:String,default:''}) url:string;
    @Prop({type:String,default:''}) html:string;

    name:string = 'editor';
    editor:any;

    mounted() {
        this.editor = new WangEditor(this.$refs.content);

        this.editor.customConfig = {
            uploadImgServer: `${ this.url }api/services/app/common/OnPostUploadImg?type=editor`,
            uploadImgMaxSize: 1 * 1024 * 1024,
            uploadImgMaxLength: 1,
            uploadFileName: 'file',
            zIndex: 1,
            uploadImgParams: {
                type: 'editor'
            },
            menus: [
                // 菜单配置
                'head', // 标题
                'bold', // 粗体
                'fontSize', // 字号
                'fontName', // 字体
                'italic', // 斜体
                'underline', // 下划线
                'strikeThrough', // 删除线
                'foreColor', // 文字颜色
                'backColor', // 背景颜色
                'link', // 插入链接
                'list', // 列表
                'justify', // 对齐方式
                'quote', // 引用
                'emoticon', // 表情
                'image', // 插入图片
                'table', // 表格
                'code', // 插入代码
                'undo', // 撤销
                'redo' // 重复
            ],
            // 编辑器的事件，每次改变会获取其html内容
            onchange: html => {
                this.editorChange(html)
            },
            customAlert: (info) => {
                this.$Notice.warning({
                    title: this.L('UploadFail'),
                    desc: info
                });
            },
            uploadImgHooks: {
                success: (xhr, editor, result) => {
                    // 图片上传并返回结果，图片插入成功之后触发
                    // xhr 是 XMLHttpRequst 对象，editor 是编辑器对象，result 是服务器端返回的结果
                },
                fail: (xhr, editor, result) => {
                    this.$Notice.warning({
                        title: this.L('UploadFail')
                    });
                },
                error: (xhr, editor) => {
                    this.$Notice.warning({
                        title: this.L('UploadFail')
                    });
                },
                customInsert: (insertImg, result, editor) => {
                    var url = `${ this.url }Upload/Editor/${ result.result.fileName }`;
                    insertImg(url);
                }
            },

        }
        this.editor.create(); // 创建富文本实例
    }

    clearContent() {
        this.editor.txt.html('');
    }

    @Watch('html', {immediate: true, deep: true})
    onChangeValue(newVal: string, oldVal: string){
        if(this.editor) {
            this.editor.txt.html(newVal)
        }
    }

    @Emit('editorChange')
    editorChange(n: string){

    }
}
</script>

<style scoped>

</style>